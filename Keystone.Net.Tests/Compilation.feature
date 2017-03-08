Feature: Compilation

@mytag
Scenario: Compile the nop instruction
	Given An instance of Keystone built for X86 in mode 32
	And The statements "nop"
	When I compile the statement with Keystone
	Then the result is 0x90

@mytag
Scenario: Compile an invalid instruction
	Given An instance of Keystone built for X86 in mode 32
	And The statements "invalid instruction"
	When I compile the statement with Keystone
	Then The last error is EXPR_TOKEN
