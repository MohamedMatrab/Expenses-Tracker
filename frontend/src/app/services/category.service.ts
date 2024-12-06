import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {CategoryResponse} from "../pages/other/categories/categories.component";
import {EntityService} from "./entity.service";
import {Result} from "../models/result";


@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = `${environment.rootUrl}/api/Categories`;
  constructor(private http:HttpClient,private entityService:EntityService) { }

  getCategories(filter?:{name?:string,sortOrder?:string,pageNumber?:number,pageSize?:number}): Observable<Result<CategoryResponse[]>> {
    const url = `${this.apiUrl}/get-categories`
    return this.entityService.getEntityList<CategoryResponse>(url,filter,true)
  }
}
