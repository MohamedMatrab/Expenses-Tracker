import { Injectable } from '@angular/core';
import {catchError, Observable, of, throwError} from "rxjs";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Result} from "../models/result";

@Injectable({
  providedIn: 'root'
})
export class EntityService{

  constructor(private http:HttpClient) { }

  getEntityList<T>(url:string, obj?:any,returnEmptyIf403?:boolean): Observable<Result<T[]>> {
    let params = new HttpParams();
    if(obj){
      Object.keys(obj).forEach(key => {
        if (obj[key] !== undefined && obj[key] !== null) {
          params = params.set(key, obj[key]);
        }
      })
    }

    const headers = new HttpHeaders({})
    return this.http.get<Result<T[]>>(`${url}`, { headers:headers,params:params }).pipe(
      catchError(err => {
        if (err.status==403 && returnEmptyIf403){
          let res:Result<T[]> = {errors:[],isSuccess:true,response:[]};
          return of(res);
        }
        return throwError(()=>err)
      })
    );
  }

  getOneEntity<T>(url:string, obj?:{[key:string]:any}):Observable<T>{
    const headers = new HttpHeaders({});
    let params = new HttpParams();
    if(obj){
      Object.keys(obj).forEach(key => {
        if (obj![key] !== undefined && obj[key] !== null) {
          params = params.set(key, obj[key]);
        }
      })
    }
    return this.http.get<T>(`${url}`, { headers:headers,params:params });
  }

  postEntity<T>(obj:T, url:string) : Observable<Result<any>>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<Result<any>>(`${url}`, obj, { headers });
  }

  putEntity<T>(obj:T, url:string) : Observable<Result<any>>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.put<Result<any>>(`${url}`, obj, { headers });
  }

  deleteEntities(ids:number[],url:string):Observable<Result<any>>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.delete<Result<any>>(`${url}`,{headers:headers,body:ids});
  }
}
