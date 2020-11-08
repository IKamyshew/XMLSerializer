using System.Xml;
using Xunit;
using Xunit.Abstractions;

namespace Enom.Test
{
    public class PushlistTests
    {
        [Fact]
        public void Test1()
        {
            XmlDocument pushList = new XmlDocument();
            pushList.Load("UpdatePushlistSuccessResponse.xml");

            string html = Pushlist.RenderUpdatePushListHtml(pushList);

            Assert.False(true, html);
        }
    }
}
