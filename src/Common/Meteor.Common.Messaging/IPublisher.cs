namespace Meteor.Common.Messaging;

public interface IPublisher<TData>
{
    public void Publish(TData body, IEnumerable<KeyValuePair<string, string>>? metadata = null);
}