Feature: Authentication

Scenario: Authentication failure
	Given [Authentication page is opened]
	When [Users logins using bad credentials]
	Then [the result should be failed]