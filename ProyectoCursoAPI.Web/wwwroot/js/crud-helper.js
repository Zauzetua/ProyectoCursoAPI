/**
 * crud-helper.js
 * 
 * Este archivo contiene la logica reutilizable de JavaScript para manejar operaciones CRUD
 * en una sola vista mediante modales de Bootstrap 5 y peticiones asincronas AJAX con jQuery.
 * 
 * Conceptos clave explicados:
 * 1. Carga de Modales: Hace GET al endpoint del controlador para traer la vista parcial (_FormModal).
 * 2. Envio de Formularios: Intercepta el submit para enviar los datos con AJAX.
 *    - Si el servidor retorna HTML: Hubo error de validacion, se reemplaza el contenido del modal sin recargar.
 *    - Si el servidor retorna JSON: Fue exitoso, se cierra el modal y se actualiza la tabla.
 * 3. Actualizacion de Tabla: Hace GET a un endpoint que regresa solo el HTML de las filas (_TablePartial).
 * 4. Paginacion AJAX: Los clics en enlaces de paginacion interceptan la recarga y piden la pagina seleccionada.
 * 5. Eliminacion con Modal: Muestra un modal de confirmacion y ejecuta un POST con antiforgery token.
 */

var CrudHelper = (function () {

    // Variable interna para guardar los parametros de eliminacion pendientes
    var deleteConfig = {
        url: '',
        id: '',
        token: '',
        onSuccess: null
    };

    /**
     * Muestra una alerta flotante temporal en la parte superior.
     * @param {string} message - Texto del mensaje a mostrar.
     * @param {string} type - Tipo de alerta Bootstrap ('success', 'danger', 'info', 'warning').
     */
    function showAlert(message, type) {
        var alertHtml = '<div class="alert alert-' + type + ' alert-dismissible fade show shadow-sm" role="alert">' +
            message +
            '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
            '</div>';

        var $container = $('#ajaxAlertContainer');
        $container.html(alertHtml);

        // Auto-cerrar despues de 4 segundos
        setTimeout(function () {
            $container.find('.alert').alert('close');
        }, 4000);
    }

    /**
     * Abre el modal generico cargando el contenido desde una URL del controlador.
     * @param {string} url - Ruta del endpoint que regresa la PartialView del formulario.
     */
    function openModal(url) {
        var $modalContent = $('#genericModalContent');
        $modalContent.html('<div class="p-5 text-center text-muted"><div class="spinner-border text-primary" role="status"></div><p class="mt-2 mb-0">Cargando...</p></div>');

        var modalElement = document.getElementById('genericFormModal');
        var modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);
        modalInstance.show();

        $.ajax({
            url: url,
            type: 'GET',
            success: function (response) {
                $modalContent.html(response);
                // Re-inicializa la validacion no obstructiva de jQuery en el formulario inyectado
                var $form = $modalContent.find('form');
                if ($form.length) {
                    $.validator.unobtrusive.parse($form);
                }
            },
            error: function () {
                $modalContent.html('<div class="p-4 text-center text-danger">Ocurrio un error al cargar el formulario.</div>');
            }
        });
    }

    /**
     * Envia el formulario del modal mediante AJAX.
     * Si hay errores de validacion (servidor regresa HTML con el Partial), se reemplaza el modal.
     * Si la operacion es exitosa (servidor regresa JSON con success=true), se cierra el modal y se actualiza la tabla.
     * @param {HTMLFormElement} formElement - Elemento form enviado.
     * @param {Function} refreshTableCallback - Funcion para refrescar la tabla al completar.
     */
    function submitModalForm(formElement, refreshTableCallback) {
        var $form = $(formElement);

        // Si la validacion en el navegador falla, no enviamos
        if (!$form.valid()) {
            return false;
        }

        var submitBtn = $form.find('button[type="submit"]');
        submitBtn.prop('disabled', true).prepend('<span class="spinner-border spinner-border-sm me-1" role="status"></span>');

        $.ajax({
            url: $form.attr('action'),
            type: $form.attr('method') || 'POST',
            data: $form.serialize(),
            success: function (response, status, xhr) {
                var contentType = xhr.getResponseHeader('content-type') || '';

                // Si el servidor respondio con JSON, la operacion fue exitosa
                if (contentType.indexOf('application/json') !== -1) {
                    if (response.success) {
                        var modalElement = document.getElementById('genericFormModal');
                        var modalInstance = bootstrap.Modal.getInstance(modalElement);
                        if (modalInstance) {
                            modalInstance.hide();
                        }

                        showAlert(response.message || 'Operacion completada con exito.', 'success');

                        if (typeof refreshTableCallback === 'function') {
                            refreshTableCallback();
                        }
                    } else {
                        showAlert(response.message || 'No se pudo completar la operacion.', 'danger');
                    }
                } else {
                    // Si el servidor respondio con HTML (la vista parcial con errores de validacion)
                    var $modalContent = $('#genericModalContent');
                    $modalContent.html(response);
                    var $newForm = $modalContent.find('form');
                    if ($newForm.length) {
                        $.validator.unobtrusive.parse($newForm);
                    }
                }
            },
            error: function () {
                showAlert('Ocurrio un error inesperado al procesar la peticion.', 'danger');
            },
            complete: function () {
                submitBtn.prop('disabled', false).find('.spinner-border').remove();
            }
        });

        return false;
    }

    /**
     * Carga el contenido parcial de una tabla mediante AJAX.
     * @param {string} containerSelector - Selector del contenedor donde se insertara el HTML de la tabla.
     * @param {string} url - URL del endpoint GetTable del controlador.
     * @param {number} page - Numero de pagina a solicitar.
     */
    function loadTable(containerSelector, url, page) {
        var $container = $(containerSelector);
        var targetUrl = url;
        if (page) {
            targetUrl += (url.indexOf('?') === -1 ? '?' : '&') + 'page=' + page;
        }

        $container.css('opacity', '0.5');

        $.ajax({
            url: targetUrl,
            type: 'GET',
            success: function (html) {
                $container.html(html);
            },
            error: function () {
                showAlert('Error al actualizar el listado de datos.', 'danger');
            },
            complete: function () {
                $container.css('opacity', '1');
            }
        });
    }

    /**
     * Abre el modal de confirmacion para eliminar un registro.
     * @param {string} url - URL del endpoint de eliminacion.
     * @param {string} id - Identificador GUID del registro a eliminar.
     * @param {string} token - Token antiforgery para la peticion POST.
     * @param {string} message - Mensaje descriptivo opcional para el modal.
     * @param {Function} onSuccess - Callback a invocar tras eliminar exitosamente.
     */
    function confirmDelete(url, id, token, message, onSuccess) {
        deleteConfig.url = url;
        deleteConfig.id = id;
        deleteConfig.token = token;
        deleteConfig.onSuccess = onSuccess;

        if (message) {
            $('#deleteModalMessage').text(message);
        } else {
            $('#deleteModalMessage').text('Esta seguro de que desea eliminar este registro?');
        }

        var modalElement = document.getElementById('confirmDeleteModal');
        var modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);
        modalInstance.show();
    }

    // Event listener para el boton "Eliminar" del modal de confirmacion
    $(document).on('click', '#btnConfirmDelete', function () {
        var $btn = $(this);
        $btn.prop('disabled', true);

        $.ajax({
            url: deleteConfig.url,
            type: 'POST',
            data: {
                id: deleteConfig.id,
                __RequestVerificationToken: deleteConfig.token
            },
            success: function (response) {
                var modalElement = document.getElementById('confirmDeleteModal');
                var modalInstance = bootstrap.Modal.getInstance(modalElement);
                if (modalInstance) {
                    modalInstance.hide();
                }

                if (response.success) {
                    showAlert(response.message || 'Registro eliminado correctamente.', 'success');
                    if (typeof deleteConfig.onSuccess === 'function') {
                        deleteConfig.onSuccess();
                    }
                } else {
                    showAlert(response.message || 'No se pudo eliminar el registro.', 'danger');
                }
            },
            error: function () {
                showAlert('Ocurrio un error al intentar eliminar el registro.', 'danger');
            },
            complete: function () {
                $btn.prop('disabled', false);
            }
        });
    });

    // Delegacion de eventos para enlaces de paginacion AJAX dentro de tablas
    $(document).on('click', '.pagination .page-link', function (e) {
        e.preventDefault();
        var $link = $(this);
        var page = $link.data('page');
        if (!page) return;

        var $tableContainer = $link.closest('[data-ajax-table="true"]');
        if ($tableContainer.length) {
            var url = $tableContainer.data('table-url');
            loadTable($tableContainer, url, page);
        }
    });

    return {
        showAlert: showAlert,
        openModal: openModal,
        submitModalForm: submitModalForm,
        loadTable: loadTable,
        confirmDelete: confirmDelete
    };

})();
