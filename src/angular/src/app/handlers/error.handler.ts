import { HttpErrorResponse } from "@angular/common/http";
import { Injectable, ErrorHandler, EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { Router } from "@angular/router";

@Injectable()
export class AppErrorHandler implements ErrorHandler {

  constructor(
    private readonly router: Router
  ) { }

  handleError(error: unknown) {

    if (console.error)
      console.error(error);

    else if (console.log)
      console.log(error);

    const em = (m: string) =>
      btoa(encodeURIComponent(m));

    let errorDocumentLocation = '/error';

    if (error instanceof HttpErrorResponse) {

      errorDocumentLocation += `?statusCode=${encodeURIComponent(error.status)}`;

      if (error.error?.message)
          errorDocumentLocation += `&message=${em(error.error.message)}`;

      else
        errorDocumentLocation += `&message=${em('Não foi possível estabelecer ligação com os serviços aplicacionais.')}`;
    }

    else {
      errorDocumentLocation += `?message=${em('Encontrámos um erro inesperado durante o processamento da última operação.')}`;

      if (error instanceof Error) {

        if (error.stack)
          errorDocumentLocation += `&stackTrace=${em(error.stack)}`;
      }
    }

    this.router.navigateByUrl(errorDocumentLocation);
  }
}

export const provideErrorHandler = (): Array<EnvironmentProviders> => [
  makeEnvironmentProviders([{
    provide: ErrorHandler,
    useClass: AppErrorHandler
  }])
];

