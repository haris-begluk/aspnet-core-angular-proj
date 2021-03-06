import { Auth } from './services/auth.service';
import { BrowserXhr } from '@angular/http';
import { BrowserXhrWithProgress, ProgressService } from './services/progress.service';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import * as Sentry from "@sentry/browser";
import { AppErrorHandler } from './app-error-handler';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleService } from './services/vehicle.service';
import { HttpModule } from '@angular/http'; 

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { PaginationComponent } from './components/pagination/pagination.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { PhotoService } from './services/photo.service'; 


Sentry.init({
  dsn: "https://c3dccfbfaaf644809993177839b211c7@sentry.io/1300187"
});
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
    ViewVehicleComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule, 
    HttpModule,
    FormsModule, 
    BrowserAnimationsModule, 
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
      { path: 'vehicles/new', component: VehicleFormComponent },
      { path: 'vehicles/edit/:id', component: VehicleFormComponent }, 
      { path: 'vehicles/:id', component: ViewVehicleComponent }, 
      { path: 'vehicles', component: VehicleListComponent }, 
      { path: 'home', component: HomeComponent}, 
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [ 
    VehicleService, 
    PhotoService,  
    Auth,
    ProgressService,
    { provide: ErrorHandler, useClass: AppErrorHandler},
    { provide: BrowserXhr, useClass: BrowserXhrWithProgress}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
