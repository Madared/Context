using Context.ResultsContext.ContextResults;
using Context.ResultsContext.ContextResults.ContextResultExtensions;

namespace Context.Tests.ResultContextTests;

public class FromSuccessResult
{
    private static readonly Func<Result<string>> ResultFunc = () => Result<string>.Ok("hello");
    private static readonly IContextResult<string> Context = ResultFunc.RunAndGetContext();

    [Fact]
    public void Context_Is_Successful()
    {
        Assert.True(Context.Succeeded);
    }

    [Fact]
    public void Mapping_With_Action_Gives_Successful_Context()
    {
        string? hello = null;
        IContextResult mapped = Context.Do(() => hello = "hello");
        Assert.True(mapped.Succeeded);
        Assert.NotNull(hello);
    }

    [Fact]
    public void Mapping_With_Success_Simple_Result_Function_Gives_Successful_Context()
    {
        IContextResult mapped = Context.Do(Result.Ok);
        Assert.True(mapped.Succeeded);
    }

    [Fact]
    public void Mapping_With_Failed_Simple_Result_Function_Gives_Failed_Context()
    {
        IContextResult<string> mapped = Context.Do(() => Result.Fail(new UnknownError()));
        Assert.True(mapped.Failed);
    }

    [Fact]
    public void Mapping_With_Not_Null_Function_Gives_Success_Context()
    {
        IContextResult<string> mapped = Context.Map(() => "new hello");
        Assert.True(mapped.Succeeded);
    }

    [Fact]
    public void Mapping_With_Not_Null_With_Input_Function_Gives_Success_Context()
    {
        IContextResult<string> mapped = Context.Map(str => $"new {str}");
        Assert.True(mapped.Succeeded);
    }

    [Fact]
    public void Mapping_With_Success_Result_Function_Gives_Successful_Context()
    {
        IContextResult<int> mapped = Context.Map(() => Result<int>.Ok(4));
        Assert.True(mapped.Succeeded);
    }

    [Fact]
    public void Mapping_With_Success_Result_Function_With_Input_Gives_Successful_Context()
    {
        IContextResult<int> mapped = Context.Map(str => Result<int>.Ok(str.Length));
        Assert.True(mapped.Succeeded);
    }

    [Fact]
    public void Mapping_With_Failed_Result_Function_Gives_Failed_Context()
    {
        IContextResult<int> mapped = Context.Map(() => Result<int>.Fail(new UnknownError()));
        Assert.True(mapped.Failed);
    }

    [Fact]
    public void Mapping_With_Failed_Result_Function_With_Input_Gives_Failed_Context()
    {
        IContextResult<int> mapped = Context.Map(str => Result<int>.Fail(new UnknownError()));
        Assert.True(mapped.Failed);
    }

    [Fact]
    public void Stripping_Context_Gives_Same_Result()
    {
        Result<string> result = ResultFunc();
        Result<string> stripped = Context.StripContext();

        Assert.Equal(result.GetType(), stripped.GetType());
        Assert.Equal(result.Succeeded, stripped.Succeeded);
        Assert.Equal(result.Data, stripped.Data);
    }
}