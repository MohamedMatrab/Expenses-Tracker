import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {BehaviorSubject, catchError, filter, from, Observable, switchMap, take, throwError} from "rxjs";
import {Router} from "@angular/router";
import {AuthService} from "../services/auth.service";
import {AuthResult} from "../models/auth/user";


@Injectable()
export class JwtInterceptor implements HttpInterceptor
{
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<AuthResult | undefined> = new BehaviorSubject<AuthResult | undefined>(undefined);
  refreshTokenSubject$ = this.refreshTokenSubject.asObservable();

  constructor(private authService:AuthService, private router:Router) {}

  private addToken(req: HttpRequest<any>, token?: string): HttpRequest<any> {
    return req.clone({
      setHeaders: {
        Authorization: 'Bearer ' + (token??'')
      }
    });
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const request$ = new Observable<HttpRequest<any>>(observer => {
      let token = this.authService.getToken()?.token;
      if(token)
        observer.next(this.addToken(request, token));
      observer.complete();
    });
    return request$.pipe(
      switchMap(req => {
        return next.handle(req);
      }),
      catchError((error: Error) => {
        if (error instanceof HttpErrorResponse) {
          switch (error.status) {
            case 401:
              return this.handle401Error(request,next);
            default:
              throw error;
          }
        }
        else {
          throw error;
        }
      })
    );
  }

  redirectLogout() {
    this.authService.logout()
    this.router.navigate(['./auth/login'])
  }

  private handle401Error(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    debugger
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(undefined)

      return from(this.authService.refreshToken()).pipe(
        switchMap((res: AuthResult) => {
          this.isRefreshing = false;
          this.refreshTokenSubject.next(res)
          return next.handle(this.addToken(req,res.token));
        }),
        catchError((error) => {
          this.redirectLogout()
          return throwError(() => error);
        })
      );
    }
    else {
      return this.refreshTokenSubject$.pipe(
        filter(result => result != undefined),
        take(1),
        switchMap((res) => next.handle(this.addToken(req, res?.token)))
      );
    }
  }
}
