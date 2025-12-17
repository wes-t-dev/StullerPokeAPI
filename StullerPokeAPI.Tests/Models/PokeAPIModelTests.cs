using StullerPokeAPI.Models;
using Xunit;

namespace StullerPokeAPI.Tests.Models
{
    public class PokeAPIModelTests
    {
        [Fact]
        public void ApiResponseTypeRelations_Default_Success_Is_False()
        {
            var resp = new ApiResponseTypeRelations();
            Assert.False(resp.Success);
            Assert.Null(resp.TypeRelations);
        }

        [Fact]
        public void ApiRequestPokemonType_AllowsNullName()
        {
            var req = new ApiRequestPokemonType();
            Assert.Null(req.PokemonName);
        }
    }
}
