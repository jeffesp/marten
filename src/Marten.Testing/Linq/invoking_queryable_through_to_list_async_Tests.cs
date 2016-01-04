﻿using System.Linq;
using System.Threading.Tasks;
using Marten.Services;
using Marten.Testing.Documents;
using Marten.Util;
using Shouldly;
using Xunit;

namespace Marten.Testing.Linq
{
    public class invoking_queryable_through_to_list_async_Tests : DocumentSessionFixture<NulloIdentityMap>
    {
        [Fact]
        public async Task use_to_list_async_in_query()
        {
            theSession.Store(new User { FirstName = "Hank" });
            theSession.Store(new User { FirstName = "Bill" });
            theSession.Store(new User { FirstName = "Sam" });
            theSession.Store(new User { FirstName = "Tom" });

            await theSession.SaveChangesAsync();

            var users = await theSession
                .Query<User>()
                .Where(x => x.FirstName == "Sam")
                .ToListAsync();
            users.Single().FirstName.ShouldBe("Sam");
        }
    }
}