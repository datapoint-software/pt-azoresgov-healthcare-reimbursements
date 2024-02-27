import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export abstract class Client {

  protected readonly abstract baseAddress: string;

  constructor(
    private readonly http: HttpClient
  ) {}

  protected delete<TResponseModel>(address: string | null, options?: ClientRequestOptions): Observable<TResponseModel> {
    return this.http.delete<TResponseModel>(
      this.path(address, options?.path),
      { ...options }
    );
  }

  protected get<TResponseModel>(address: string | null, options?: ClientRequestOptions): Observable<TResponseModel> {
    return this.http.get<TResponseModel>(
      this.path(address, options?.path),
      { ...options }
    );
  }

  protected post<TRequestModel, TResponseModel>(address: string | null, model: TRequestModel, options?: ClientRequestOptions): Observable<TResponseModel> {
    return this.http.post<TResponseModel>(
      this.path(address, options?.path),
      model,
      { ...options }
    );
  }

  private path(address: string | null, path?: { [ key: string ]: string }): string {

    let result = `${this.baseAddress}${address || ''}`;

    if (path)
      for (let k in path)
        result = result.replace(`:${k}`, encodeURIComponent(path[k]))

    return result;
  }

}

export type ClientRequestOptions = {
  params?: HttpParams | { [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean>; };
  path?: { [ key: string ]: string };
};
