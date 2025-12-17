using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using StullerPokeAPI.Services;
using StullerPokeAPI.Models;
using Xunit;
using System.Threading;
using System;
using System.Collections.Generic;

namespace StullerPokeAPI.Tests.Services
{
    public class PokeAPIServiceTests
    {
        private HttpClient CreateHttpClientWithResponse(string responseContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = statusCode,
                   Content = new StringContent(responseContent)
               })
               .Verifiable();

            var client = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://pokeapi.co/api/v2/")
            };

            return client;
        }

        [Fact]
        public async Task SendPokemonTypeRequestAsync_ReturnsTypes_WhenResponseIsValid()
        {
            string json = "{ \"name\": \"bulbasaur\", \"types\": [ { \"slot\": 1, \"type\": { \"name\": \"grass\", \"url\": \"https://pokeapi.co/api/v2/type/12/\" } } ] }";
            var client = CreateHttpClientWithResponse(json);
            var service = new PokeAPIService(client);

            var result = await service.SendPokemonTypeRequestAsync("bulbasaur");

            Assert.NotNull(result);
            Assert.True(result.ContainsKey("grass"));
            Assert.Equal("https://pokeapi.co/api/v2/type/12/", result["grass"]);
        }

        [Fact]
        public async Task SendPokemonTypeRequestAsync_ReturnsNull_OnNotFound()
        {
            string json = "Not Found";
            var client = CreateHttpClientWithResponse(json, HttpStatusCode.NotFound);
            var service = new PokeAPIService(client);

            var result = await service.SendPokemonTypeRequestAsync("missingmon");

            Assert.Null(result);
        }

        [Fact]
        public async Task SendPokemonTypeRelationsRequestAsync_ReturnsTypeRelations_WhenResponseIsValid()
        {
            string json = "{ \"damage_relations\": { \"double_damage_from\": [], \"double_damage_to\": [], \"half_damage_from\": [], \"half_damage_to\": [], \"no_damage_from\": [], \"no_damage_to\": [] } }";
            var client = CreateHttpClientWithResponse(json);
            var service = new PokeAPIService(client);

            var types = new Dictionary<string, string> { { "grass", "https://pokeapi.co/api/v2/type/12/" } };
            var result = await service.SendPokemonTypeRelationsRequestAsync(types);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.TypeRelations);
        }

        [Fact]
        public async Task SendPokemonTypeRelationsRequestAsync_ReturnsNull_OnNotFound()
        {
            string json = "Not Found";
            var client = CreateHttpClientWithResponse(json, HttpStatusCode.NotFound);
            var service = new PokeAPIService(client);

            var types = new Dictionary<string, string> { { "grass", "https://pokeapi.co/api/v2/type/12/" } };
            var result = await service.SendPokemonTypeRelationsRequestAsync(types);

            Assert.Null(result);
        }
    }
}
