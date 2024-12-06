import { Component } from '@angular/core';
import {NgIf} from "@angular/common";
import {LoaderService} from "../../services/loader.service";
import {MatProgressBar} from "@angular/material/progress-bar";
import {MatProgressSpinner} from "@angular/material/progress-spinner";

@Component({
  selector: 'app-loader',
  standalone: true,
  templateUrl: 'loader.component.html',
  imports: [
    NgIf,
    MatProgressBar,
    MatProgressSpinner
  ],
  styleUrl:'loader.component.scss'
})
export class LoaderComponent {
  isLoading = false;
  constructor(private loaderService: LoaderService) {
    this.loaderService.loading$.subscribe(isLoading => {
      this.isLoading = isLoading;
    })
  }
}
