<?php
namespace ANSR\Services\Importer\ImportStrategy;

/**
 * Interface ImportStrategy
 * @package ANSR\Services\Importer\ImportStrategy
 * @author Ivan Yonkov <ivanynkv@gmail.com>
 */
interface ImportStrategy
{
    /**
     * @param mixed $data
     * @return void
     */
    public function import($data);
}