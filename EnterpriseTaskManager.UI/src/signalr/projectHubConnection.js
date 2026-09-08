import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

const connection = new HubConnectionBuilder()
  .withUrl('https://localhost:7184/hubs/projects')
  .withAutomaticReconnect()
  .configureLogging(LogLevel.Information)
  .build();

export default connection;
