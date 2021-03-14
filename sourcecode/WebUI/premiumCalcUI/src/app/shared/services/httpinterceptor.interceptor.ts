
import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router'
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable()
export class HttpServiceInetrceptor implements HttpInterceptor {
  constructor(private router: Router) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    console.log('HttpServiceInetrceptor');

    return next.handle(request)
      .pipe(catchError((errorResponse: HttpErrorResponse) => {
        console.log('HttpServiceInetrceptor error');

        if (errorResponse instanceof HttpErrorResponse) {
          if (errorResponse.status == 400) {
            return throwError(errorResponse.error);
          }
          else {
            this.router.navigate(["error/"]);
          }
        }
        return throwError(errorResponse);
      }));
  }
}
 