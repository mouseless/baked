namespace Do.Test.CodeGeneration;

public class CompilerErrors : TestServiceSpec
{
    [Test]
    public void Compiler_throws_error_with_compiler_errors_and_code()
    {
        var compiler = GiveMe.ACompiler(code: "this is not c#");

        var compile = () => compiler.Compile();

        compile.ShouldThrow<Exception>().Message.ShouldContain("this is not c#");
    }
}
