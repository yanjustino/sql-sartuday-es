namespace IntegratedTests.Commons;

public class ApiFixture
{
    private WebApplicationFactory<Program> _factory = null!;
    
    public Task InitializeAsync(string connectionString)
    {
        var configs = new Dictionary<string, string>
        {
            ["connectionString"] = connectionString
        };        
        
        var build = new ConfigurationBuilder()
            .AddInMemoryCollection(configs.AsReadOnly()!).Build();
        
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseConfiguration(build));
        
        return Task.CompletedTask;
    } 
    
    public HttpClient GetClient() => _factory.CreateClient();  
    
    public async Task DisposeAsync() => await _factory!.DisposeAsync();
}