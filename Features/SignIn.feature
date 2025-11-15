@login @ui
Feature: SignIn Feature
  As an authenticated user
  I want to sign in with valid credentials
  So that I can access my dashboard successfully

  @positive
  Scenario: Successful Login
    Given I navigate to the Mars portal login page
    When I enter valid username and password
    And I click on the login button
    Then I should be redirected to the dashboard
    And the user name should be displayed on the top right corner


