using StullerPokeAPI.Models;
using Xunit;

namespace StullerPokeAPI.Tests.Models
{
    public class TypeRelationsTests
    {
        [Fact]
        public void TypeRelations_Defaults_Are_NotNull()
        {
            var tr = new TypeRelations();
            Assert.NotNull(tr.DoubleDamageFrom);
            Assert.NotNull(tr.DoubleDamageTo);
            Assert.NotNull(tr.HalfDamageFrom);
            Assert.NotNull(tr.HalfDamageTo);
            Assert.NotNull(tr.NoDamageFrom);
            Assert.NotNull(tr.NoDamageTo);
        }
    }
}
