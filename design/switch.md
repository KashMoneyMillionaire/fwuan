# Switch

 - Switches are `restrictions` that live at the block level rather than the method level.
 - A switch must cover all possible cases or a compiler error will be thrown.
 - `default` can be used as a catch-all at the end to cover all remaining scenarios
 - Regex restrictions are not used for type-restricting in the overall case.
 - The order of the matching restrictions (cases/ranges/expressions) matters. The restrictions run from top to bottom until they match.
 - There is no need to `break` from a case because cases do not fall through. Ranges or other guards can be used to handle multiple