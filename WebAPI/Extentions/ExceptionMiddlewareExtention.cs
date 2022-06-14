// namespace WebAPI.Extentions
// {
//     public static class ExceptionMiddlewareExtention
//     {
//         public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (app.Environment.IsDevelopment())
//             {
//                 app.UseSwagger();
//                 app.UseSwaggerUI();
//                 app.UseDeveloperExceptionPage();
//             }
//             else
//             {
//                 app.UseExceptionHandler(
//                     options =>
//                     {
//                         options.Run(
//                             async context =>
//                             {
//                                 context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//                                 var ex = context.Features.Get<IExceptionHandlerFeature>();

//                                 if (ex != null)
//                                 {
//                                     await context.Response.WriteAsync(ex.Error.Message);
//                                 }
//                             }
//                         );
//                     }
//                 );
//             }
//         }
//     }
// }