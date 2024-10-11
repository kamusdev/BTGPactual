import packageInfo from '../../package.json';

export const environment = {
  appVersion: packageInfo.version,
  production: true,
  apiUrl: 'https://xi6jt5ngc4.execute-api.us-east-1.amazonaws.com/dev'
};
