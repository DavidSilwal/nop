export const environment = {
  production: false,
  application: {
    name: 'Jewellery',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44336',
    clientId: 'Jewellery_App',
    dummyClientSecret: '1q2w3e*',
    scope: 'Jewellery',
    showDebugInformation: true,
    oidc: false,
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44336',
    },
  },
  localization: {
    defaultResourceName: 'Jewellery',
  },
};
