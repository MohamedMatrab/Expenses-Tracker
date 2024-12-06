import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {LoaderComponent} from "./components/loader/loader.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoaderComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'Modernize Angular Admin Template';

  // constructor(private loader:LoaderService) {
  //   loader.showLoader();
  // }
}
