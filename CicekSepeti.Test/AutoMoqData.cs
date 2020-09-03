using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CicekSepeti.Test
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        [Obsolete]
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
