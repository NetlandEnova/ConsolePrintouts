# StyleGuid

## C#

### Naming rules

#### Rule summary:
- Names of classes, methods, enumerations, public fields, public properties, namespaces: `PascalCase`.
- Names of local methods, variables, parameters: `camelCase`.
- Names of private, protected, internal and protected internal fields and properties: `camelCase`.
- Names of public static readonly fields and const fields: `SNAKE_CASE`.
- For casing, a “word” is anything written without internal spaces, including acronyms. For example, `MyRpc` instead of ~~MyRPC~~.
- Names of interfaces start with `I`, e.g. `IYourNameOfInterface`. 
  
#### Files
- Filenames and directory names are `PascalCase`, e.g. `MyFile.cs`.
- File name should be always the same as the name of the main class in the file, e.g. `MyClass.cs`.
- One core class per file.
- Namespace must match file/folder layout (project structure)

#### Organization
- Modifiers occur in the following order: `public protected internal private new abstract virtual override sealed static readonly extern unsafe volatile async`.
- Namespace `using` declarations go at the top, before any namespaces. `using` import order is alphabetical, apart from `System` imports which always go first. `using static` go at the end.
- Class member ordering:
  - Group class members in the following order:
    - Delegates and events.
    - Static, const and readonly fields.
    - Fields.
    - Constructors and finalizers.
    - Properties.
    - Methods.
    - Nested classes and enums.
  - Within each group, elements should be in the following order:
    - Public.
    - Internal.
    - Protected internal.
    - Protected.
    - Private.

### Coding guidelines

#### Constants
- If `const` isn’t possible, `readonly` can be a suitable alternative.
- Prefer named constants to magic numbers.

#### Null checks
- Prefer `is null` or `is not null` convention over standard operation checks.

### The 'var' keyword
- Use of `var` is encouraged if it aids readability by avoiding type names that are noisy, obvious, or unimportant.
- Encouraged:
  - When the type is obvious - e.g. `var apple = new Apple();`, or `var request = Factory.Create<HttpRequest>();`
  - When working with basic types - e.g. `var success = true;`
- Discouraged:
  - When working with compiler-resolved built-in numeric types - e.g. `var number = 12 * ReturnsFloat();`
  - When users would clearly benefit from knowing the type - e.g. `var listOfItems = GetList();`

### Whitespace rules
- A maximum of one statement per line.
- A maximum of one assignment per statement.
- Indentation of **4** spaces, no tabs.
- Column limit: **120**.
- Line break before opening brace.
- Line break between closing brace and `else`.
- Space after `if/for/while` etc., and after commas.
- For function definitions and calls, if the arguments do not all fit on one line they should be broken up onto multiple lines, with each subsequent lines with a four space indent. The code example below illustrates this.

### Property styles
- For single line read-only properties, prefer expression body properties (`=>`) when possible.
- For everything else, use the older `{ get; set; }` syntax.

### Lambdas (and if statements) vs named methods
- If a lambda is non-trivial (e.g. more than a couple of statements, excluding declarations), or is reused in multiple places, it should probably be a named method
- Same goes for `if` statements. If statement is non-trivial it should be a named method.

### ref and out
- Use `out` for returns that are not also inputs.
- Place `out` parameters after all other parameters in the method definition.
- `ref` should be used rarely, when mutating an input is necessary.
- Do not use `ref` as an optimisation for passing structs.
- Do not use `ref` to pass a modifiable container into a method. `ref` is only required when the supplied container needs be replaced with an entirely different container instance.

### LINQ
- In most cases, prefer named method or extension over single line LINQ calls when statement is hard to read e.g. is non-trivial (no matter if LINQ is long or short).
- Prefer member extension methods over SQL-style LINQ keywords - e.g. prefer `myList.Where(x)` over `myList where x`.
- Avoid `Container.ForEach(...)` for anything longer than a single statement.
- Try to keep LINQ query calls in provided order:
```c#
linqQuery
    .IgnoreQueryFilters()
    .AsNoTracking/AsTracking
    .AsSplitQuery/AsSingleQuery
    .Include
        .ThenInclude
            .ThenInclude
                (...etc)
    .Where
    .Skip
    .Take
    .OrderBy
```

### Array vs List
- In general, prefer `List<>` over arrays for public variables, properties, and return types.
- Prefer `List<>` when the size of the container can change.
- Prefer arrays when the size of the container is fixed and known at construction time.
- Prefer array for multidimensional arrays.

### Folders and file locations
- Be consistent with the project.

### String interpolation vs `String.Format()` vs `String.Concat` vs `operator+`
- In general, use whatever is easiest to read, particularly for logging and assert messages.
- Be aware that chained `operator+` concatenations will be slower and cause significant memory churn.
- If performance is a concern, `StringBuilder` will be faster for multiple string concatenations.

### Functions
- Do not add empty functions. It will be treated as a code smell. Instead of doing this rethink your class architecture.

### Example code
```c#
using System;                                           // `using` goes at the top.
                                                        // Indent after usings.
namespace MyNamespace                                   // Namespaces are PascalCase.
{                                                       // In namespaces opening brace goes to new line.
    public interface IMyInterface                       // Interfaces start with 'I'.
    {                                                   // In interfaces opening brace goes to new line.
        int Calculate(float value, float exp);          // Methods are PascalCase without 'public' keyword.
    }

    public enum MyEnum                                  // Enumerations are PascalCase.
    {                                                   // In enumerations opening brace goes to new line.
        Yes,                                            // Enumerators are PascalCase.
        No,                                             // Comma after last enumartor.
    }

    public class MyClass                                // Classes are PascalCase.
    {                                                   // In classes opening brace goes to new line.
        private class SomeClass
        {
            public int NumNegativeResults = 0;
            public int NumPositiveResults = 0;
            public int NumNeutralResults = 0;
        }
        
        private readyonly SomeClass incomes;            // Private member variables are camelCase.
        private readyonly SomeClass outcomes;           // Members are arainged in 'logic blocks'
        
        public static int value = 0;
        private const int tax = 23;                     // 'const' does not affect naming convention.
        
        public IDictionary<int, int> someDictionary1 = new Dictionary<int, int>
        {                                               // In object init. opening brace goes to new line.
            {1, 2}, {3, 4}, {5, 6}                      // Squashe members if in column limit.
        };
        private Dictionary<SomeClass, SomeClass> someDictionary2 = new()
        {
            {                                           // Choop members if out of coulmn limit.
                new()
                {
                    NumNegativeResults = 1,
                    NumPositiveResults = 2,
                    NumNeutralResults = 3
                },
                new()
                {
                    NumNegativeResults = 4,
                    NumPositiveResults = 5,
                    NumNeutralResults = 6
                }
            }
        };
                                                        // Line break before constructor.
        public MyClass(                                 // Opening bracket in same line.
            SomeClass incomes,                          // First argument goes to new line.
            SomeClass outcomes,
            int koo,
            bool moo
        ) : base(koo, moo)                              // Closing bracket in new line with base init.
        {
            this.incomes = incomes;
            this.outcomes = outcomes;
        }
        
        public MyClass(SomeClass some) : base("a", "b") // Base init. and parameters in same line if not out of limit.
        {
            incomes = some;                             // Do not use "this" keyword if no need to.
            outcomes = some;
        }

        public int CalculateValue(int mulNumber)        // Function parameters are camelCase.
        {                                               // In functions opening brace goes to new line.
            int resultValue = Foo * mulNumber;          // Local variables are camelCase.
            NumTimesCalled++;
            Foo += bar;

            var isFooValid = Foo > 123;                 // Space indent around the operators.
            if (isWholeValid(isFooValid))               // No space between used in statemnt variable.
            {                                           // In 'if' opening brace goes to new line.
                incomes.NumNegativeResults++;
            }
            else if (
                resultValue > 0 &&                      // First condition goes to new line (if chooping).
                isSomeValidFunction() ||                // Condition operators are placed on line end.
                (resultValue > 10 && resultvalue < 5)
            )                                           // Choop parameters if out of limit.
            {                                           // In 'else/else-if' opening brace in new line.
                return Foo;
            }
 
            return resultValue;                         // Line break before return statement. (As it is separate logic block)
        }

        public void ExpressionBodies()
        {
            // For simple lambdas, fit on one line if possible, no brackets or braces required.
            Func<int, int> increment = x => x + 1;

            // Closing brace aligns with first character on line that includes the opening brace.
            Func<int, int, long> difference1 = (x, y) => {
                long z = 123;
                long diff = (long)x - y + z;
                
                return diff >= 0 ? diff : -diff;
            };

            // If needed choop lambda as shown:
            SomeFireFunction(
                "Message to enemy",
                (x, y) => {
                    long z = 123;
                    long diff = (long)x - y + z;

                    return diff >= 0 ? diff : -diff;
                }
            );

            // [BAD EXAMPLE] If you are chooping members, choop them all.
            SomeFireFunction("Message to enemy",        // This should go to new line as shown above.
                (x, y) => {
                    long z = 123;
                    long diff = (long)x - y + z;

                    return diff >= 0 ? diff : -diff;
                }
            );

            // [GOOD EXAMPLE] If you are not chooping first level of memebers. You can do as shown:
            SomeFireFunction("Message to enemy", (x, y) => {
                long z = 123;
                long diff = (long)x - y + z;

                return diff >= 0 ? diff : -diff;
            });
        }

        protected bool ExampleMagicFunction()
        {
            // Example logic block (function with decalred above parameters)
            var veryLongArgumentName = false;
            var apple = new Apple();
            AppleItem item = getItem(apple);            // Call function without adding line break (logic block).
            AnotherLongFunctionNameThatCausesLineWrappingProblems(
                apple,
                item,
                veryLongArgumentName
            );
                                                        // Add line break before logic block
            foreach(var someStrangeBook in GetItems())
            {                                           // In loop statements opening brace goes to new line.
                var magicSpell = someStrangeItem.Where(
                    x => x.NestedItem.Where(
                        n =>
                            n.IsSpell &&                // If you are chooping, choop them all.
                            myCustomValidMethod(
                                veryLongArgumentName,   // If you rae chooping, choop them all.
                                !veryLongArgumentName,
                                true
                            )
                    )
                ).DoSomeTricks()
                .GetOneValidSpell();
                                                        // Add line break before logic block.
                RenewMana();
                ChargCakra();
                                                        // Add line break before logic block.
                try
                {                                       // In 'try/catch' statements opening brace goes to new line.
                    magicSpell.SetName("PrettyName")    // Max to one "dot function call" per line.
                        .SetVeryLongArgumentName(veryLongArgumentName)
                        .CastSpell();
                }
                catch (MagicException e)
                {
                    ErrorBook.WriteToBook($"Broken skill {e.Name}");

                    return false;
                }
            }
            
            return true;
        }
    }
}
```