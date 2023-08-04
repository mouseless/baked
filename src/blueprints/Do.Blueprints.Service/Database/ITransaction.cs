namespace Do.Database;

// flat: commits existing and begins new transaction, returns result in current session
// nested: creates new session, begins and commits a new transaction, reloads result in parent session
// mock: does nothing but hitting mock so that it can be verified
public interface ITransaction
{
    Task<T> CommitAsync<T>(Func<T> action);
}
