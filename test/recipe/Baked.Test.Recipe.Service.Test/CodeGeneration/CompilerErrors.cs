namespace Baked.Test.CodeGeneration;

public class CompilerErrors : TestServiceSpec
{
    [Test]
    public void Compiler_throws_error_with_compiler_errors_and_code()
    {
        var compiler = GiveMe.ACompiler(code: "this is not c#");

        var compile = () => compiler.Compile(string.Empty);

        compile.ShouldThrow<Exception>().Message.ShouldContain("this is not c#");
    }

    [Test]
    public void Compiler_throws_error_with_closest_method_decleration()
    {
        var compiler = GiveMe.ACompiler(code: """
        using System;

        public class Sample
        {
            public string FooMethod()
            {
                string x = "asd";
            }
        }
        """);

        var compile = () => compiler.Compile(string.Empty);

        var message = compile.ShouldThrow<Exception>().Message;
        message.ShouldContainWithoutWhitespace("""
        public string FooMethod()
        {
            string x = "asd";
        }
        """);
        message.ShouldNotContain("public class Sample");
    }

    [Test]
    public void Compiler_throws_error_with_closest_class_decleration()
    {
        var compiler = GiveMe.ACompiler(code: """
        using System;

        public class Sample
        {
            public @string Property { get; set;}
        }
        """);

        var compile = () => compiler.Compile(string.Empty);

        compile.ShouldThrow<Exception>().Message.ShouldContainWithoutWhitespace("""
        public class Sample
        {
            public @string Property { get; set;}
        }
        """);
    }
}