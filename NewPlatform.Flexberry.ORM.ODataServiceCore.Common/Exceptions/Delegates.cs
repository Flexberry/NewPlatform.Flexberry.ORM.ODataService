namespace NewPlatform.Flexberry.ORM.ODataServiceCore.Common.Exceptions
{
    using System;
    using System.Net;

    /// <summary>
    /// Type of delegate called after global exception occured.
    /// </summary>
    /// <param name="ex"> Exceprtion that occured during processing of Odata request.</param>
    /// <param name="code">Returning Error Code. By default 500.</param>
    /// <returns>Exception for client.</returns>
    public delegate Exception FilterDelegateAfterInternalServerError(Exception ex, ref HttpStatusCode code);
}
