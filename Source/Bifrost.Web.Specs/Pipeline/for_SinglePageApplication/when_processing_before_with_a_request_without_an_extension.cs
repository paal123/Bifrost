using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Specs.Pipeline.for_SinglePageApplication
{
	public class when_processing_before_with_a_request_without_an_extension : given.a_single_page_application_and_a_web_context_with_default_routes
	{
		Establish context = () => web_request_mock.SetupGet(c=>c.Path).Returns("/something/going/on");
		
		Because of = () => single_page_application.Before(web_context_mock.Object);
		
		It should_redirect_to_index = () => web_context_mock.Verify(w=>w.RewritePath("/index.html"), Times.Once());
	}
}

