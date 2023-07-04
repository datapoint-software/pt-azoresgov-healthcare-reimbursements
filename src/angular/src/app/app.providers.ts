import { AppErrorHandler } from "./app.handlers";
import { EnvironmentProviders, ErrorHandler, makeEnvironmentProviders } from "@angular/core";

export const provideErrorHandler = (): Array<EnvironmentProviders> => [
  makeEnvironmentProviders([{
    provide: ErrorHandler,
    useClass: AppErrorHandler
  }])
];
