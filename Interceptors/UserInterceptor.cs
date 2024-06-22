using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using User_EFC_Interceptor.Models.Entities;
using User_EFC_Interceptor.Services.Base64;

namespace User_EFC_Interceptor.Interceptors
{
    internal sealed class UserInterceptor : SaveChangesInterceptor, IMaterializationInterceptor
    {
        private readonly IBase64Service _base64Service;

        public UserInterceptor(IBase64Service base64Service)
        {
            _base64Service = base64Service;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result, 
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context != null)
            {
                UpdateUserEntityBeforeSaveToDatabase(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateUserEntityBeforeSaveToDatabase(DbContext db)
        {
            var entries = db.ChangeTracker
                .Entries<User>()
                .Select(e => e.Entity)
                .ToList();

            foreach (var entry in entries)
            {
                entry.Phrase = _base64Service.Encode(entry.Phrase);
            }
        }

        public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
        {
            UpdateUserEntityAfterGetFromDatabase(entity);
            return entity;
        }

        private void UpdateUserEntityAfterGetFromDatabase(object entity)
        {
            if (entity is User e)
            {
                e.Phrase = _base64Service.Decode(e.Phrase);
            }
        }
    }
}
