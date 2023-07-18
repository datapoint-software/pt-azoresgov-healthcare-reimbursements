import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandler, Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable()
export class AppErrorHandler implements ErrorHandler{

  constructor(
    private readonly router: Router
  ) {}

  handleError(error: unknown) {

    if (console.error)
      console.error(error);

    else if (console.log)
      console.log(error);

    const em = (m: string) =>
      btoa(encodeURIComponent(m));

    let errorDocumentLocation = '/error';

    if (error instanceof HttpErrorResponse) {

      errorDocumentLocation += `?status=${encodeURIComponent(error.status)}&message=`;

      if ('object' === typeof error.error
        && error.error.source === 'app'
        && error.error.message)
          errorDocumentLocation += em(error.error.message);

      else if ([ 503, 504 ].indexOf(error.status) > -1)
        errorDocumentLocation += em('Não foi possível estabelecer comunicação com os serviços aplicacionais.');
    }

    else {
      errorDocumentLocation += `?message=${em('Encontrámos um erro inesperado durante o processamento da última operação.')}`
    }

    this.router.navigateByUrl(errorDocumentLocation);
  }
}
