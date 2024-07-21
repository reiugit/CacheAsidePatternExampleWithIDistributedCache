namespace CacheAsidePatternExampleWithIDistributedCache;

public record ResponseWithTimestamp(string Response, DateTimeOffset CachedAt);
