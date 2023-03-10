using Meteor.Common.Core.Models;

namespace Meteor.Common.Core.Services.Abstractions
{
    public interface IAsyncValidator<TModel>
    {
        public Task<bool> TryValidateAsync(TModel employee, ICollection<ValidationError> errors);
    }
}
