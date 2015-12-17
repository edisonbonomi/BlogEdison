$(document).ready(function () {
    $('.excluir-post').on('click', function(e) {
        //se não confirmar
        if(!confirm('Deseja realmente excluir esse post?')) 
        {
            e.preventDefault();
        }

    })

    $('.excluir-comentario').on('click', function (e) {
        //se não confirmar
        if (!confirm('Deseja realmente excluir esse comentário?')) {
            e.preventDefault();
        }

    })

});

