import {CommonModule, NgOptimizedImage} from '@angular/common';
import {Component, ViewChild} from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MaterialModule } from 'src/app/material.module';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import {CategoryDialogComponent} from "./category-dialog/category-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort, Sort} from "@angular/material/sort";
import {CategoryService} from "../../../services/category.service";
import {ActivatedRoute} from "@angular/router";

export interface Category {
  description: string
  name: string
}

export interface CategoryResponse {
  id: string
  description: string
  monthTotal: number
  name: string
}

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [
    MatTableModule,
    CommonModule,
    MatCardModule,
    MaterialModule,
    MatIconModule,
    MatMenuModule,
    MatButtonModule,
    NgOptimizedImage,
  ],
  templateUrl: './categories.component.html',
})
export class AppCategoriesComponent {

  constructor(private dialog: MatDialog,private categoryService:CategoryService,private activatedRoute:ActivatedRoute) {
    let list = activatedRoute.snapshot.data["categories"];
  }

  displayedColumns1: string[] = ['name', 'expenses_amount','description', 'budget'];
  dataSource1 : CategoryResponse[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {

    } else {

    }
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(CategoryDialogComponent, {
      width: '500px',
      data: { name: '', description: '' }
    })
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        console.log('Dialog result:', result);
      }
    })
  }

}
