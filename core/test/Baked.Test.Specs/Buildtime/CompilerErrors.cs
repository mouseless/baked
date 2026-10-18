namespace Baked.Test.Buildtime;

public class CompilerErrors : TestSpec
{
    [Test]
    public void Compiler_throws_error_with_compiler_errors_and_code()
    {
        var compiler = GiveMe.ACompiler(code: "this is not c#");

        var compile = () => compiler.Compile(GiveMe.ALocation());

        compile.ShouldThrow<Exception>().Message.ShouldContain("this is not c#");
    }

    [Test]
    public void Compiler_error_starts_with_assembly_name_and_error_count()
    {
        var compiler = GiveMe.ACompiler(code: """
        public class Sample
        {
            public int Foo() => "foo";
            public int Bar() => "bar";
        }
        """);

        var compile = () => compiler.Compile(GiveMe.ALocation());

        var message = compile.ShouldThrow<Exception>().Message;
        message.Split(Environment.NewLine)[0].ShouldBe("`Baked.g.Test` failed with 2 compile error(s)");
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

        var compile = () => compiler.Compile(GiveMe.ALocation());

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

        var compile = () => compiler.Compile(GiveMe.ALocation());

        compile.ShouldThrow<Exception>().Message.ShouldContainWithoutWhitespace("""
        public class Sample
        {
            public @string Property { get; set;}
        }
        """);
    }
}