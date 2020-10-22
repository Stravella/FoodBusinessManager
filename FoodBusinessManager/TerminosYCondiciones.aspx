<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="TerminosYCondiciones.aspx.vb" Inherits="FoodBusinessManager.TerminosYCondiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="lblTerminosyCondiciones" runat="server" Text="Términos y Condiciones"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitCondicionesUso" runat="server" Text="CONOCIMIENTO Y ACEPTACIÓN DE LAS CONDICIONES DE USO" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblCondicionesUso" CssClass="text-justify" runat="server" Text="El servicio que Affiniti Solutions brinda al usuario (en adelante 'Usuario') lo es en virtud de los términos y condiciones del presente Acuerdo de Términos y Condiciones del servicio y de conformidad con las normas de operación y disposiciones publicadas periódicamente. El presente documento supone el acuerdo completo entre el Usuario y el Titular y prevalece sobre cualquier acuerdo anterior suscrito por las partes en relación con el objeto del presente. El presente aviso de información legal regula el uso del servicio ofrecido y el Titular pone a disposición de los usuarios de Internet a sus efectos."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitDescripcionServicio" runat="server" Text="DESCRIPCIÓN DEL SERVICIO" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblDescripcionServicio" CssClass="text-justify" runat="server" Text="Los servicios prestados por Affiniti Solutions son de venta de membresias a productos través de Internet. El Titular pone a disposición del Usuario el servicio web de carácter gratuito para la información y escaparate virtual de los productos. Los precios de los productos están fijados en cada artículo y son visibles antes de la adquisición del producto desde nuestro servicio. Los precios reflejados en la página son los precios definitivos salvo error tipográfico. Los productos en oferta o con descuento aparecen marcados como tal, indicándose el correspondiente descuento aplicado. Todos los precios están expresados en pesos argentinos. El Titular se reserva el derecho de modificar el precio de cualquier producto sin previo aviso. El Titular podrá, en un futuro, proporcionar al Usuario nuevos contenidos, servicios, productos o facilidades adicionales, sean o no gratuitas, que incrementen las prestaciones disponibles para el Usuario. A su vez, el Titular se reserva el derecho de cancelar unilateralmente cualquiera de los contenidos, servicios o utilidades incorporadas al servicio."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitEnlancesExternos" runat="server" Text="ENLACES EXTERNOS" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblEnlacesExternos" CssClass="text-justify" runat="server" Text="El Titular no se hace responsable por la edición, revisión y censura de la información, ni se compromete a verificar el contenido de las páginas o sitios con los cuales el Usuario se conecte a través del servicio. Por consiguiente, Affiniti Solutions no se hace responsable de la verificación del cumplimiento de las normas que protegen los Derechos de Autor, de la legalidad o de la decencia del contenido de las páginas a las que se tenga acceso a través del servicio, tampoco será responsable de los banners mostrados en las listas de enlaces del servicio, ya que estos no son propiedad del Titular."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitModificacionesCondicionesServicio" runat="server" Text="MODIFICACIONES EN LAS CONDICIONES DEL SERVICIO" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblModificacionesCondicionesServicio" CssClass="text-justify" runat="server" Text="El Usuario acepta que el Titular podrá, cuando lo considere conveniente, realizar correcciones, mejoras o modificaciones en la Información o los Servicios, sin que ello de lugar, ni derecho a ninguna reclamación o indemnización, ni implique reconocimiento de responsabilidad alguna. La utilización ininterrumpida de Affiniti Solutions por parte del Usuario constituirá una ratificación del presente documento, con las modificaciones y cambios que se hubieran introducido."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitModificacionesServicio" runat="server" Text="MODIFICACIONES EN EL SERVICIO" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblModificacionesServicio" CssClass="text-justify" runat="server" Text="Por las mismas razones anteriormente mencionadas, el Titular se reserva el derecho de modificar o interrumpir el Servicio en todo o en parte, habiendo mediado o no notificación al Usuario. El Titular no será responsable ante el Usuario ni ante terceros por haber ejercitado su derecho de modificar o interrumpir el Servicio."> </asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitExoneracionResponsabilidad" runat="server" Text="EXONERACIÓN DE RESPONSABILIDAD" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblExoneracionResponsabilidad" CssClass="text-justify" runat="server" Text="El usuario acepta expresamente asumir exclusivamente todo riesgo proveniente de la utilización del servicio. El servicio se brinda sobre la base de 'Así como está' y 'Según esté disponible'. El titular no garantiza que el servicio responda a los requisitos del usuario ni que el servicio no sea interrumpido ni que sea seguro, oportuno o exento de errores, como tampoco asegura los resultados que se obtengan de la utilización del servicio, ni la exactitud o confiabilidad de la información obtenida a través del servicio. Tampoco garantiza la corrección de defectos en el servicio. El titular expresamente niega haber ofrecido cualquier tipo de garantía , ya sea explícita o implícita, incluidas las garantías implícitas provenientes del título, aptitud para la venta, adecuación para un fin determinado y no contravención. El usuario declara haber comprendido y aceptado que cualquier material y/o información descargados del sistema corre por su cuenta y riesgo y que ha de ser el único responsable por los daños que pudieran causar en su sistema informático o por la pérdida de datos ocasionada por la descarga de material y/o información. El titular no garantiza ninguno de los bienes ni servicios adquiridos u obtenidos a través del servicio ni las transacciones realizadas a través del servicio. Ninguna recomendación ni información obtenidas por un usuario, directamente  de FBM o a través del servicio, ya sea en forma oral o escrita, podrá constituirse en garantía del titular si no ha sido asumido aquí expresamente."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitLimitacionResponsabilidad" runat="server" Text="LIMITACION DE RESPONSABILIDAD" Font-Bold="true"></asp:Label>

                                </p>
                                <p>
                                    <asp:Label ID="lblLimitacionResponsabilidad" CssClass="text-justify" runat="server" Text="El titular no se responsabiliza por la imposibilidad de uso, la interrupción de negocios ni los daños directos o indirectos, especiales, incidentales, o consecuentes de cualquier tipo (incluida la pérdida de beneficios) sin tener en cuenta la forma en que el hecho hubiera tenido lugar, ya fuera éste contractual, culposo (incluyendo negligencia), responsabilidad por el producto o de cualquier otra forma, aun en el supuesto de que se hubiera advertido a Affiniti Solutions de la posibilidad de tales daños."></asp:Label>

                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitConductaUsuario" runat="server" Text="CONDUCTA DEL USUARIO" Font-Bold="true"></asp:Label>

                                </p>
                                <p>
                                    <asp:Label ID="lblConductaUsuario" CssClass="text-justify" runat="server" Text="El Usuario es el único responsable por el contenido de las transmisiones a través del Servicio. La utilización del servicio por parte del Usuario está sometida a las leyes y reglamentos locales, provinciales, nacionales e internacionales. El Usuario acepta: (1) no utilizar el Servicio con fines ilícitos, ni los prohibidos en este documento; (2) no interferir en los sistemas de redes conectados con el Servicio ni desarticularlos; (3) atenerse a todos los reglamentos, disposiciones y procedimientos de los sistemas de redes conectados con el Servicio. El Usuario no obstaculizará el uso del Servicio de otro Usuario ni el uso de servicios similares por parte de otra entidad. El titular podrá, según su propio criterio, dar por terminado el servicio en forma inmediata, si la conducta del usuario no fuera acorde con estos términos y condiciones."></asp:Label>

                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitDerechosContenido" runat="server" Text="DERECHOS DE PROPIEDAD DEL CONTENIDO" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblDerechosContenido" CssClass="text-justify" runat="server" Text="El Usuario reconoce que el contenido, incluido pero no limitado a él, de textos, software, música, sonido, fotografías, vídeo, ilustraciones y otro material que se encuentre presentada en el Servicio (‘Contenido’), por el Titular o por los proveedores del Titular, está amparado por los derechos de propiedad intelectual, patentes y marcas comerciales y registradas, marcas de servicios, y otros derechos derivados de la propiedad intelectual o industrial; por ello, se faculta al Usuario a utilizar este Contenido en la forma en que lo autoricen expresamente el Servicio. Se le prohíbe al Usuario copiar, reproducir, distribuir o realizar creaciones basadas en este Contenido sin que Affiniti Solutions lo autorice expresamente. El Usuario se obliga a no descomponer ni alterar el software del Affiniti Solutions por concepto alguno, ni permitir que terceros lo realicen."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblTitTerminosGenerales" runat="server" Text="TERMINOS GENERALES" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblTerminosGenerales" CssClass="text-justify" runat="server" Text="El presente acuerdo se regirá por la Ley argentina, sometiéndose las partes para cualquier cuestión litigiosa derivada del presente acuerdo a los Juzgados y Tribunales de la Ciudad Autónoma de Buenos Aires, Argentina. El Usuario no cederá derecho ni obligación alguna derivada del presente acuerdo, salvo que medie el consentimiento expreso por escrito del Titular. Cualquier intento de cesión del Acuerdo sin el mencionado consentimiento será nulo y carecerá de todo efecto. No obstante lo anterior, el Titular tendrá derecho a ceder el presente acuerdo con todos sus derechos y obligaciones ya sea por venta del servicio, disolución, escisión, fusión de empresas o de cualquier otra forma de transmisión. Este contrato será de aplicación obligatoria y tendrá efecto entre las partes y sus respectivos representantes, herederos, administradores, sucesores y titulares de transmisiones autorizadas, salvo lo aquí establecido. Si un tribunal competente considerara que alguna disposición o disposiciones del presente acuerdo fuese contraria a la ley, tal o tales disposiciones serán redactadas nuevamente de forma tal que reflejen lo más fielmente posible las intenciones de las partes, mientras las otras disposiciones se mantendrán vigentes y aplicables. En el supuesto de que alguna de las cláusulas del presente Acuerdo resultara inválida o inaplicable, la parte válida o aplicable y las restantes disposiciones del Acuerdo se mantendrán vigentes y aplicables. Toda renuncia al derecho de reclamación (explícita o implícita) de cualquiera de las partes a cualquier infracción del presente Acuerdo no constituirá renuncia al derecho a reclamación por otra infracción o por una infracción subsiguiente. No se renunciará a ninguna disposición del Acuerdo por acto, omisión o desconocimiento de una de las partes o de sus representantes o empleados, sino por medio de un instrumento escrito y firmado en el cual se renuncie expresamente a dicha disposición. Los títulos de las cláusulas en el presente acuerdo se utilizan únicamente para la comodidad de las partes y carecen de significación legal o contractual."></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>
</asp:Content>
