using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KhelljyrBuilder
{
    public abstract class SyntaxExplorationResolver
    {
        public abstract void Run(MemberDeclarationSyntax member);
    }

    public class SyntaxExplorationResolver<T> : SyntaxExplorationResolver where T : MemberDeclarationSyntax
    {
        private readonly Action<T> Action;

        public SyntaxExplorationResolver(Action<T> action)
        {
            Action = action;
        }

        public override void Run(MemberDeclarationSyntax member)
        {
            Action((T)member);
        }
    }
}
