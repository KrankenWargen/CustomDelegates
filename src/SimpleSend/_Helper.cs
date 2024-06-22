using System.Linq.Expressions;
using System.Reflection;

namespace FGW.Farm;

public static class Helper
{
    public static Type GetDelegateType(MethodInfo methodInfo)
    {
        var paramTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();

        var delegateType = methodInfo.ReturnType == typeof(void)
            ? Expression.GetActionType(paramTypes)
            : Expression.GetFuncType(paramTypes.Append(methodInfo.ReturnType).ToArray());

        return delegateType;
    }
}