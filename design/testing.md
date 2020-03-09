# Testing (maybe just unit tests?)

### Testing code is just as important as writing code and therefore should be easy to write, run, document, and discover

Because the type system allows for stronger guarantees around parameters and return values, a lot of boilerplate testing can
be removed. The goals of the testing platform are to allow tests to be run anytime, with timely feedback to the code
you're currently working on. Thus, they need to be
 - side-effect free
 - highly parallelizable
 - fast
 
 
 
 ## IDE/Testing Env
  - include tests in same feature folder as the rest of the code
  - make tests "different" so they aren't included in release buidls
  - make tests "knowledgable" so they have access to private parts of code
  - make declaration of test include name of code being testing, strongly typed
    - allows actions like "run all tests related to this code"
    - code coverage?
  - allow easy mutation testing
