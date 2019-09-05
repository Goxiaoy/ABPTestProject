export const environment = {
  production: false,
  hmr: true,
  application: {
    name: 'TestProject',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44368',
    clientId: 'TestProject_App',
    dummyClientSecret: '1q2w3e*',
    scope: 'TestProject',
    showDebugInformation: true,
    oidc: false,
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44313',
    },
  },
  localization: {
    defaultResourceName: 'TestProject',
  },
};
