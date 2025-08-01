using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Handlers.CodeRefactor.Rewriters
{
    public class ReplaceAttributeValueRewriter : CSharpSyntaxRewriter
    {
        private string _attributeName;
        private string _propertyName;
        private string _argumentValue;

        public ReplaceAttributeValueRewriter(string attributeName,
                                             string propertyName,
                                             string argumentValue)
        {
            _attributeName = attributeName;
            _propertyName = propertyName;
            _argumentValue = argumentValue;
        }

        public override SyntaxNode? VisitAttribute(AttributeSyntax node)
        {
            //D:/Test/TestApiAnnotation search "[Authorize]" update-prop "Roles" new-value "admin"

            if (node.Name.ToFullString() == _attributeName)
            {
                if (node.ArgumentList is null)
                    return node;

                var oldArgument = node.ArgumentList.Arguments
                    .FirstOrDefault(argument => argument.NameEquals?.Name
                    .ToString() == _propertyName);

                if (oldArgument is null)
                    return node;

                var newExpression = SyntaxFactory
                    .LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory
                    .Literal(_argumentValue));

                var newArgument = oldArgument.WithExpression(newExpression);

                var newArguments = node.ArgumentList.Arguments.Replace(oldArgument, newArgument);

                var newArgumentList = node.ArgumentList.WithArguments(newArguments);

                return node.WithArgumentList(newArgumentList);
            }
            return base.VisitAttribute(node);
        }
    }
}
