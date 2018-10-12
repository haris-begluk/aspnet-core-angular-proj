import * as Sentry from "@sentry/browser";
import { ToastrService } from 'ngx-toastr';
import { ErrorHandler, Inject, NgZone, isDevMode } from '@angular/core'; 
export class AppErrorHandler implements ErrorHandler { 
    constructor(private ngZone: NgZone,
        @Inject(ToastrService) private toastr:ToastrService){

    }
    handleError(error: any):void{  
        //To test this if-else statment set environment variable to production and run application once again 
        //then the errors on our web site will be logged to sentry application 
        if(!isDevMode())
        Sentry.captureException(error.originalError || error); 
        else  
        throw error;
        this.ngZone.run(() => {
            console.log('ERROR'); 
            this.toastr.error('An error happend!', "Error", {timeOut:5000});
        });
        
   
    }
} 
