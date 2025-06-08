namespace Baked.Test.ExceptionHandling;

public class ShowUnhandledFlag : TestServiceSpec
{
    [Test]
    public void It_adds_error_details_when_flag_is_up()
    {
        var handler = GiveMe.AnUnhandledExceptionHandler(showUnhandled: true);

        var actual = handler.Handle(new("UNHANDLED"));

        actual.ExtraData!["message"].ShouldBe("UNHANDLED");
    }

    [Test]
    public void It_hides_error_details_when_flag_is_down()
    {
        var handler = GiveMe.AnUnhandledExceptionHandler(showUnhandled: false);

        var actual = handler.Handle(new());

        actual.ExtraData!.ShouldBeNull();
    }

    [Test]
    public void Client_exception_handler_handles_when_flag_is_up()
    {
        var handler = GiveMe.AClientExceptionHandler(showUnhandled: true);

        var actual = handler.CanHandle(GiveMe.AClientException());

        actual.ShouldBeTrue();
    }

    [Test]
    public void Client_exception_is_handled_as_500()
    {
        var handler = GiveMe.AClientExceptionHandler(showUnhandled: true);

        var actual = handler.Handle(GiveMe.AClientException());

        actual.Code.ShouldBe(500);
    }

    [Test]
    public void Client_exception_message_is_in_body()
    {
        var handler = GiveMe.AClientExceptionHandler(showUnhandled: true);
        var exception = GiveMe.AClientException(message: "INNER");

        var actual = handler.Handle(exception);

        actual.Body.ShouldBe("INNER");
    }

    [Test]
    public void Client_exception_content_is_in_extra_data()
    {
        var handler = GiveMe.AClientExceptionHandler(showUnhandled: true);
        var exception = GiveMe.AClientException(content: """
        {
            "test": "content"
        }
        """);

        var actual = handler.Handle(exception);

        actual.ExtraData?.ShouldContainKey("content");
        actual.ExtraData?["content"].ShouldDeeplyBe(new { test = "content" }, useSystemTextJson: true);
    }

    [Test]
    public void Client_exception_handler_does_not_handle_when_flag_is_down()
    {
        var handler = GiveMe.AClientExceptionHandler(showUnhandled: false);

        var actual = handler.CanHandle(GiveMe.AClientException());

        actual.ShouldBeFalse();
    }
}