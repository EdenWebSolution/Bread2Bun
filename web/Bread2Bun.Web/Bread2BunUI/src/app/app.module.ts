import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './token-interceptor';
import { AuthorizeModule } from './modules/authorize/authorize.module';
import { SharedModule } from './modules/shared/shared.module';
import { RouterModule } from '@angular/router';
import { LayoutModule } from './modules/layout/layout.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    AuthorizeModule,
    SharedModule,
    RouterModule,
    LayoutModule,
    InfiniteScrollModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

// platformBrowserDynamic().bootstrapModule(AppModule);

