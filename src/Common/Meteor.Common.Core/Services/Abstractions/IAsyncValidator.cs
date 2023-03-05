using Meteor.Common.Core.Models;

namespace Meteor.Common.Core.Services.Abstractions
{
    public interface IAsyncValidator<TModel>
    {
        public Task<bool> TryValidateAsync(TModel model, out ICollection<ValidationError> errors);
    }
}
