import { HttpInterceptorFn } from '@angular/common/http';

import Swal from 'sweetalert2';

import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req,next)=>{

return next(req).pipe(

catchError(error=>{

Swal.fire({

icon:'error',

title:'Error',

text:error.error.message

});

return throwError(()=>error);

})

);

};