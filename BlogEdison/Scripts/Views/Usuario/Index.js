$(document).ready(function () {
    $('.excluir-usuario').on('click', function (e) {
        //se não confirmar
        if (!confirm('Deseja realmente excluir o usuário?')) {
            e.preventDefault();
        }

    })
});

