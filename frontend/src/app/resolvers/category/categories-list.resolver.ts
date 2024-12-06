import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Injectable} from "@angular/core";
import {CategoryResponse} from "../../pages/other/categories/categories.component";
import {CategoryService} from "../../services/category.service";
import {LoaderService} from "../../services/loader.service";
import {finalize, map, Observable, switchMap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CategoriesListResolver implements Resolve<CategoryResponse[]> {

    constructor(private categoryService: CategoryService, private loader: LoaderService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CategoryResponse[]> {
    this.loader.showLoader();
    debugger
    return this.categoryService.getCategories({}).pipe(
      map((res) => res.response),
      finalize(() => this.loader.hideLoader())
    );
  }


}
