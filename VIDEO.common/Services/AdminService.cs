using System.Text.Json;

namespace VIDEO.common.Services;

public class AdminService : IAdminService
{
	private readonly MembershipHttpClient _http;
	public AdminService(MembershipHttpClient http)
	{
		_http = http;
	}

	public async Task<List<TDto>> GetAsync<TDto>(string uri)
		where TDto : class
	{
		try
		{
			//using HttpResponseMessage response = await _http.Client.GetAsync(uri);
			//response.EnsureSuccessStatusCode();

			//var Debug = response.Content.ReadAsStringAsync();
			//var json = Debug.Result;

			var response = await _http.Client.GetAsync(uri);

			var debug3 = response.Content.ReadAsStreamAsync();

            var result = JsonSerializer.Deserialize<List<TDto>>(
				await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				});

			if (result == null) return new List<TDto>();

			return result;
		}
		catch (Exception ex)
		{
			return new List<TDto>();
		}
	}
}
