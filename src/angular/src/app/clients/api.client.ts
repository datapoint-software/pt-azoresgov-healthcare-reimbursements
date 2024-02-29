import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export abstract class Client {

  protected readonly abstract baseAddress: string;

  constructor(
    private readonly http: HttpClient
  ) {}

  protected delete<TResponseModel>(address: string, options?: ClientRequestOptions): Observable<TResponseModel> {
    return this.http.delete<TResponseModel>(
      this.path(address, options?.path),
      this.options({ ...options })
    );
  }

  protected get<TResponseModel>(address: string, options?: ClientRequestOptions): Observable<TResponseModel> {
    return this.http.get<TResponseModel>(
      this.path(address, options?.path),
      this.options({ ...options })
    );
  }

  protected post<TRequestModel, TResponseModel>(address: string, model: TRequestModel, options?: ClientRequestOptions): Observable<TResponseModel> {
    return this.http.post<TResponseModel>(
      this.path(address, options?.path),
      model,
      this.options({ ...options })
    );
  }

  private options(options: ClientRequestOptions) {

    if (options.params && !(options.params instanceof HttpParams)) {
      for (const k in options.params) {
        if (!options.params[k]) {
          delete options.params[k];
        }
      }
    }

    return options as {
      params?: HttpParams | { [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean>; };
      path?: { [ key: string ]: string };
    };
  }

  private path(address: string, path?: { [ key: string ]: string }): string {

    let result = `${this.baseAddress}${address}`;

    if (path)
      for (let k in path)
        result = result.replace(`:${k}`, encodeURIComponent(path[k]))

    return result;
  }

}

export interface ClientRequestOptions {
  params?: HttpParams | { [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean> | undefined; };
  path?: { [ key: string ]: string };
};
