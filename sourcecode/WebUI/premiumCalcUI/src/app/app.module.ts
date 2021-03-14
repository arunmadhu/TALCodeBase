import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatDatepickerModule, MatNativeDateModule, MatSelectModule  } from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PremiumComponent } from './premium/premium.component';
import { AboutComponent } from './about/about.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataService } from './shared/services/data.service';
import { ErrorComponent } from './error/error.component';
import { HttpServiceInetrceptor } from './shared/services/httpinterceptor.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    PremiumComponent,
    AboutComponent,
    HeaderComponent,
    FooterComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    NgbModule,
    HttpClientModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpServiceInetrceptor, multi: true },
    DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
